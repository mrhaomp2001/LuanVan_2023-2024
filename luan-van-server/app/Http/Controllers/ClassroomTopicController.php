<?php

namespace App\Http\Controllers;

use App\Models\ClassroomTopic;
use App\Http\Requests\StoreClassroomTopicRequest;
use App\Http\Requests\UpdateClassroomTopicRequest;
use App\Models\ClassroomTopicLike;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;
use Illuminate\Validation\Rules\File;

class ClassroomTopicController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(StoreClassroomTopicRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(ClassroomTopic $classroomTopic)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(ClassroomTopic $classroomTopic)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateClassroomTopicRequest $request, ClassroomTopic $classroomTopic)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(ClassroomTopic $classroomTopic)
    {
        //
    }

    public function getTopics(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_id' => 'required|exists:classrooms,id',
                'per_page' => 'required'
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
                'classroom_id.required' => 'classroom_id.required',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $topics = ClassroomTopic::where("classroom_id", $request->classroom_id)
            ->where("topic_status_id", "1")
            ->orderBy('created_at', 'DESC')
            ->simplePaginate($request->per_page);

        foreach ($topics as $topic) {
            $topic->user;
            $topic->comment_count = count($topic->comments);
            $topic->like_up = ClassroomTopicLike::where("classroom_topic_id", $topic->id)->where("like_status", 1)->count();
            $topic->like_down = ClassroomTopicLike::where("classroom_topic_id", $topic->id)->where("like_status", -1)->count();
            $topic->like_status = ClassroomTopicLike::where("classroom_topic_id", $topic->id)->where("user_id", $request->user_id)->first();
        }

        return response()->json(['data' => $topics], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getOldTopics(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_id' => 'required|exists:classrooms,id',
                'per_page' => 'required',
                'date' => 'required',
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $topics = ClassroomTopic::where("classroom_id", $request->classroom_id)
            ->where("topic_status_id", "1")
            ->where('created_at', '<', $request->date)
            ->orderBy('created_at', 'DESC')
            ->simplePaginate($request->per_page);

        foreach ($topics as $topic) {
            $topic->user;
            $topic->comment_count = count($topic->comments);
            $topic->like_up = ClassroomTopicLike::where("classroom_topic_id", $topic->id)->where("like_status", 1)->count();
            $topic->like_down = ClassroomTopicLike::where("classroom_topic_id", $topic->id)->where("like_status", -1)->count();
            $topic->like_status = ClassroomTopicLike::where("classroom_topic_id", $topic->id)->where("user_id", $request->user_id)->first();
        }

        return response()->json(['data' => $topics], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function uploadATopic(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_id' => 'required|exists:classrooms,id',
                'topic_status_id' => 'required',
                'title' => 'sometimes|max:128',
                'content' => 'required|min:1|max:256',
                'image' => [
                    'sometimes',
                    File::image()
                        ->min(1)
                        ->max(64 * 1024)
                ],
            ],
            [
                'content.required' => 'Nội dung không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
                'title.max' => 'Tiêu đề tối đa 128 ký tự',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $topic = ClassroomTopic::create(
            [
                'user_id' => $request->user_id,
                'classroom_id' => $request->classroom_id,
                'topic_status_id' => $request->topic_status_id,
                'content' => $request->content,
            ]
        );

        if (isset($request->title)) {
            $topic->title = $request->title;
            $topic->save();
        } else {
            $topic->title = "";
            $topic->save();
        }

        if (isset($request->image)) {
            Storage::disk('public')->putFileAs("topics", $request->image, $topic->id . '.png');
        }

        return response()->json(['data' => $topic], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function updateATopic(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'id' => 'required|exists:classroom_topics,id',
                'topic_status_id' => 'required',
                'title' => 'sometimes|max:128',
                'content' => 'required|min:1|max:256',
                'image' => [
                    'sometimes',
                    File::image()
                        ->min(64)
                        ->max(64 * 1024)
                ],
            ],
            [
                'content.required' => 'Content không được rỗng',
                'content.min' => 'Nội dung tối thiểu 1 ký tự',
                'content.max' => 'Nội dung tối đa 256 ký tự',
                'title.max' => 'Tiêu đề tối đa 128 ký tự',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $topic = ClassroomTopic::updateOrCreate(
            [
                'id' => $request->id,
            ],
            [
                'topic_status_id' => $request->topic_status_id,
                'content' => $request->content,
            ]
        );

        if (isset($request->title)) {
            $topic->title = $request->title;
            $topic->save();
        } else {
            $topic->title = "";
            $topic->save();
        }

        if (isset($request->image)) {
            Storage::disk('public')->putFileAs("topics", $request->image, $topic->id . '.png');
        } else {
            Storage::disk('public')->delete("topics/" . $topic->id . '.png');
        }

        return response()->json(['data' => $topic], 200, [], JSON_UNESCAPED_UNICODE);
    }
}