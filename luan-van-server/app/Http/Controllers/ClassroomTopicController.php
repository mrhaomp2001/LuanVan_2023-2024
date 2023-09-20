<?php

namespace App\Http\Controllers;

use App\Models\ClassroomTopic;
use App\Http\Requests\StoreClassroomTopicRequest;
use App\Http\Requests\UpdateClassroomTopicRequest;
use App\Models\ClassroomTopicLike;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

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
                'content' => 'required',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'per_page.required' => 'Phải có số phần tử trên trang',
                'classroom_id.required' => 'classroom_id.required',
                'content.required' => 'classroom_id.required',
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
                'content' => 'required',
            ],
            [
                'per_page.required' => 'Phải có số phần tử trên trang',
                'content.required' => 'classroom_id.required',
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

        return response()->json(['data' => $topic], 200, [], JSON_UNESCAPED_UNICODE);
    }
}