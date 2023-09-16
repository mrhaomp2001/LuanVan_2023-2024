<?php

namespace App\Http\Controllers;

use App\Models\Classroom;
use App\Http\Requests\StoreClassroomRequest;
use App\Http\Requests\UpdateClassroomRequest;
use Illuminate\Support\Facades\Validator;

class ClassroomController extends Controller
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
    public function store(StoreClassroomRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Classroom $classroom)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Classroom $classroom)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateClassroomRequest $request, Classroom $classroom)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Classroom $classroom)
    {
        //
    }

    public function updateApi(UpdateClassroomRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'id' => 'required|exists:questions,id',
                'name' => 'required',
                'description' => 'required',
                'theme_color' => 'required',
            ],
            [
                'id.required' => 'Id không được để trống',
                'name.required' => 'Content không được rỗng',
                'description.required' => 'User Id không được rỗng',
                'theme_color.required' => 'User Id không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $classroom = Classroom::find($request->id);
        $classroom->name = $request->name;
        $classroom->description = $request->description;
        $classroom->theme_color = $request->theme_color;

        $classroom->save();

        return response()->json(['data' => $classroom], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
