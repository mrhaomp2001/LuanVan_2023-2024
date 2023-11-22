<?php

namespace App\Http\Controllers;

use App\Models\Classroom;
use App\Models\Comment;
use App\Models\Friend;
use App\Models\Message;
use App\Models\Notification;
use App\Models\Post;
use App\Models\PostLike;
use App\Models\QuestionCollection;
use App\Models\SystemNotification;
use App\Models\User;
use Carbon\Carbon;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;
use App\Models\Question;
use Illuminate\Http\Request;

class GameApiController extends Controller
{

    public function testApi(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => "required",
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        $user = User::find($request->user_id);
        return response()->json(['data' => $user], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getHome(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }


        $posts = Post::orderBy('created_at', 'DESC')->where('post_status_id', '1')->limit(3)->get();

        foreach ($posts as $post) {
            $post->postTemplate;
            $post->user;
            $post->comment_count = Comment::where("post_id", $post->id)->where('comment_status_id', "1")->count();
            $post->post_likes_up = PostLike::where("post_id", $post->id)->where("like_status", 1)->count();
            $post->post_likes_down = PostLike::where("post_id", $post->id)->where("like_status", -1)->count();
            $post->like_status = PostLike::where("post_id", $post->id)->where("user_id", $request->user_id)->first();
        }

        $notifications_not_filtered = Notification::where("user_id", $request->user_id)->orderByDesc("created_at")->get();

        $notifications = collect();
        $notification_count = 0;

        foreach ($notifications_not_filtered as $notification) {
            if (!isset($notification->model->error)) {
                $notifications->add($notification);
                $notification_count++;

                if ($notification_count >= 3) {
                    break;
                }
            }
        }

        $system_notifications = SystemNotification::orderBy("id","desc")->where("can_use", true)->get();

        return response()->json(
            [
                'posts' => $posts,
                'notifications' => $notifications,
                'system_notifications' => $system_notifications,
            ]
            ,
            200,
            [],
            JSON_UNESCAPED_UNICODE
        );
    }

    public function getQuestions(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'question_collection_id' => 'required|exists:question_collections,id',
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $questionCollection = QuestionCollection::find($request->question_collection_id);

        $questions = Question::where('question_collection_id', $request->question_collection_id)
            ->where("question_status_id", 1)
            ->limit($questionCollection->questions_per_time)
            ->inRandomOrder()
            ->get();

        foreach ($questions as $question) {
            $question->answersInRandomOrder;
        }
        return response()->json(['data' => $questions], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function register(Request $request)
    {

        $input = $request->all();

        $validator = Validator::make(
            $input,
            [
                'name' => 'required|min:3|max:32',
                'username' => 'required|min:6|unique:users,username',
                'password' => 'required|min:6|max:64'
            ],
            [
                'name.required' => 'phải nhập tên',

                'username.required' => 'phải nhập tài khoản',
                'username.unique' => 'tài khoản đã có người dùng',
                'username.min' => 'ít nhất 6 ký tự',

                'password.required' => 'phải nhập mật khẩu'
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()->first()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $user = User::create([
            'name' => $request->name,
            'username' => $request->username,
            'password' => Hash::make($request->password),
        ]);

        return response()->json(['data' => $user], 200, [], JSON_UNESCAPED_UNICODE);
    }
    public function login(Request $request)
    {
        $input = $request->all();

        $validator = Validator::make(
            $input,
            [
                'username' => 'required',
                'password' => 'required'
            ],
            [
                'username.required' => 'phải có tài khoản',
                'password.required' => 'phải có mật khẩu'
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $user = User::where('username', $request->username)->first();

        if (!isset($user->username)) {
            return response()->json(['message' => "không có tài khoản"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        if ($user->is_ban) {
            return response()->json(['message' => "Tài khoản này đã bị chặn"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        // new messages
        $user_id = $user->id;
        $has_new_message = false;

        $messages = DB::table('messages')
            ->select(DB::raw('MAX(created_at) as latest_message_date'))
            ->where('sender_id', $user_id)
            ->orWhere('receiver_id', $user_id)
            ->groupBy(DB::raw('IF(sender_id = ' . $user_id . ', receiver_id, sender_id)'))
            ->get();

        $messageIds = [];

        foreach ($messages as $message) {
            $messageIds[] = $message->latest_message_date;
        }

        $latestMessages = Message::whereIn('created_at', $messageIds)
            ->orderByDesc("created_at")
            ->get();

        $userLoginDate = Carbon::parse($user->updated_at);

        if (isset($latestMessages) && count($latestMessages) > 0) {

            $latestMessageDate = Carbon::parse($latestMessages[0]->created_at);

            if ($userLoginDate->greaterThan($latestMessageDate)) {
                $has_new_message = false;
            } else {
                $has_new_message = true;
            }
        } else {
            $has_new_message = false;
        }

        // friend

        $friends = Friend::where("user_id", $user_id)->where("friend_status_id", "3")->get();
        $has_new_friends = false;

        if (isset($friends)) {
            if (count($friends) > 0) {
                $has_new_friends = true;
            }
        }

        $user->touch();

        if (Hash::check($request->password, $user->password)) {

            return response()->json(
                [
                    'data' => $user,
                    'has_new_message' => $has_new_message,
                    'has_new_friends' => $has_new_friends,
                ]
                ,
                200,
                [],
                JSON_UNESCAPED_UNICODE
            );
        }
        return response()->json(['message' => "Sai mật khẩu"], 200, [], JSON_UNESCAPED_UNICODE);

    }

    public function getUserByLatestLogin(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'per_page' => 'required',
                'user_id' => 'required|exists:users,id',
            ],
            [
                'per_page.required' => 'phải có số phần tử trên một trang cụ thể',
                'user_id.required' => 'User Id không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $users = User::orderBy('updated_at', 'DESC')->simplePaginate($request->per_page);

        return response()->json(['data' => $users], 200, [], JSON_UNESCAPED_UNICODE);
    }
}