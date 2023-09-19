<?php

namespace App\Http\Controllers;

use App\Models\ClassroomTopicLike;
use App\Http\Requests\StoreClassroomTopicLikeRequest;
use App\Http\Requests\UpdateClassroomTopicLikeRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class ClassroomTopicLikeController extends Controller
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
    public function store(StoreClassroomTopicLikeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(ClassroomTopicLike $classroomTopicLike)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(ClassroomTopicLike $classroomTopicLike)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateClassroomTopicLikeRequest $request, ClassroomTopicLike $classroomTopicLike)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(ClassroomTopicLike $classroomTopicLike)
    {
        //
    }

    public function updateTopicLike(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_topic_id' => 'required|exists:classroom_topics,id',
                'status' => 'required',
            ],
            [
                'user_id.required' => 'user_id.required',
                'user_id.exists' => 'user_id.exists',
                'classroom_topic_id.required' => 'classroom_topic_id.required',
                'classroom_topic_id.exists' => 'classroom_topic_id.exists',
                'per_page.required' => 'Phải có số phần tử trên trang',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $like = ClassroomTopicLike::updateOrCreate(
            [
                'user_id' => $request->user_id,
                'classroom_topic_id' => $request->classroom_topic_id
            ],
            [
                'like_status' => $request->status,
            ]
        );

        return response()->json(['data' => $like], 200, [], JSON_UNESCAPED_UNICODE);
    }
}