<?php

namespace App\Http\Controllers;

use App\Models\Classroom;
use App\Models\QuestionCollection;
use App\Models\User;
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
                'data' => "required",
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        return response()->json(['data' => count($request->input('data'))], 200, [], JSON_UNESCAPED_UNICODE);
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
                'name' => 'required',
                'username' => 'required|min:6|unique:users,username',
                'password' => 'required'
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
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
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
                'username' => 'required|',
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

        $user->touch();

        if (Hash::check($request->password, $user->password)) {

            return response()->json(
                [
                    'data' => $user,
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