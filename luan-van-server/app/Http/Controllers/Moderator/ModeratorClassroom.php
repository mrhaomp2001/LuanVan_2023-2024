<?php

namespace App\Http\Controllers\Moderator;

use App\Http\Controllers\Controller;
use App\Models\Classroom;
use Illuminate\Http\Request;

class ModeratorClassroom extends Controller
{
    //
    public function index(Request $request)
    {
        $classrooms = Classroom::where("user_id", auth()->user()->id)->get();

        return view("moderators.classroom-index")->with(['classrooms' => $classrooms]);
    }

    public function create()
    {
        return view("moderators.classroom-create");
    }

    public function store(Request $request)
    {
        $currentClassCount = Classroom::where("user_id", auth()->user()->id)->count();

        if ($currentClassCount >= auth()->user()->max_classroom_count) {
           return view("errors.error")->with(['error' => 'Bạn đã tạo quá nhiều lớp học', 'message' => 'Bạn đã tạo quá số lượng lớp học cho phép']);
        }

        
    }
}