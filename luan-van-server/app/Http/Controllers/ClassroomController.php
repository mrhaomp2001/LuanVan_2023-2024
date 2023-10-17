<?php

namespace App\Http\Controllers;

use App\Models\Classroom;
use App\Http\Requests\StoreClassroomRequest;
use App\Http\Requests\UpdateClassroomRequest;
use App\Models\Question;
use App\Models\QuestionCollection;
use App\Models\StudyClassroom;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;

class ClassroomController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
        $data = Classroom::paginate(7);
        return view("classrooms.index")->with("data", $data);
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
    public function show($id)
    {
        //
        $classroom = Classroom::find($id);
        $questionCollections = QuestionCollection::where("classroom_id", $id)->paginate(9);

        if (!isset($classroom)) {
            return redirect(route('classrooms.index'));
        }

        return view("classrooms.edit")
            ->with("classroom", $classroom)
            ->with("questionCollections", $questionCollections);
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
    public function update(UpdateClassroomRequest $request)
    {

        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'classroom_id' => 'required|exists:classrooms,id',
                'name' => 'required',
                'description' => 'required',
            ],
            [
                'classroom_id.required' => 'classroom_id.required',
                'classroom_id.exists' => 'classroom_id.exists',
            ]
        );

        if ($validator->fails()) {
            return $validator->errors();
        }

        // return $request;
        $classroom = Classroom::find($request->classroom_id);

        $classroom->name = $request->name;
        $classroom->description = $request->description;
        $classroom->save();

        return redirect()->route('classrooms.index');
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

    public function getClassrooms(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'per_page' => 'required',
            ],
            [
                'per_page.required' => 'phải có số phần tử trên một trang cụ thể',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $classrooms = Classroom::orderBy('created_at', 'DESC')->simplePaginate($request->per_page);

        foreach ($classrooms as $classroom) {

            if (Storage::disk('public')->exists('classrooms/avatars/' . $classroom->id . ".png")) {
                $classroom->avatar_path = Storage::url('classrooms/avatars/' . $classroom->id . ".png");
            } else {
                $classroom->avatar_path = "";
            }

        }

        return response()->json(['data' => $classrooms], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getOldClassrooms(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'per_page' => 'required',
                'date' => 'required',
            ],
            [
                'per_page.required' => 'phải có số phần tử trên một trang cụ thể',
                'date.required' => 'phải có ngày bắt đầu lấy',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $classrooms = Classroom::where('created_at', '<', $request->date)->orderBy('created_at', 'DESC')->simplePaginate($request->per_page);

        foreach ($classrooms as $classroom) {

            if (Storage::disk('public')->exists('classrooms/avatars/' . $classroom->id . ".png")) {
                $classroom->avatar_path = Storage::url('classrooms/avatars/' . $classroom->id . ".png");
            } else {
                $classroom->avatar_path = "";
            }
        }

        return response()->json(['data' => $classrooms], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getUserClassrooms(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
            ],
            [
                'user_id.required' => 'user_id.required',
                'user_id.exists' => 'user_id.exists',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $classrooms = StudyClassroom::where('user_id', $request->user_id)
            ->where('study_status_id', 1)
            ->get();

        foreach ($classrooms as $classroom) {
            $classroom->classroom;
            if (Storage::disk('public')->exists('classrooms/avatars/' . $classroom->classroom->id . ".png")) {
                $classroom->classroom->avatar_path = Storage::url('classrooms/avatars/' . $classroom->classroom->id . ".png");
            } else {
                $classroom->classroom->avatar_path = "";
            }
        }

        return response()->json(['data' => $classrooms], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getClassroomInfo(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_id' => 'required|exists:classrooms,id'
            ],
            [
                'user_id.required' => 'user_id.required',
                'user_id.exists' => 'user_id.exists',
                'classroom_id.required' => 'classroom_id.required',
                'classroom_id.exists' => 'classroom_id.exists',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $classroom = Classroom::find($request->classroom_id);

        if (Storage::disk('public')->exists('classrooms/avatars/' . $classroom->id . ".png")) {
            $classroom->avatar_path = Storage::url('classrooms/avatars/' . $classroom->id . ".png");
        } else {
            $classroom->avatar_path = "";
        }

        $studyStatus = StudyClassroom::where("classroom_id", $request->classroom_id)
            ->where("user_id", $request->user_id)
            ->first();

        $classroom->questionCollectionsOpen;
        $classroom->study_status = $studyStatus;

        return response()->json(['data' => $classroom], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function updateStudyStatus(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_id' => 'required|exists:classrooms,id',
                'status' => 'required'
            ],
            [
                'user_id.required' => 'user_id.required',
                'user_id.exists' => 'user_id.exists',
                'classroom_id.required' => 'classroom_id.required',
                'classroom_id.exists' => 'classroom_id.exists',
                'status.required' => 'status.required',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $status = StudyClassroom::updateOrCreate(
            [
                'user_id' => $request->user_id,
                'classroom_id' => $request->classroom_id,
            ],
            [
                'study_status_id' => $request->status,
            ]
        );

        $status->classroom;

        return response()->json(['data' => $status], 200, [], JSON_UNESCAPED_UNICODE);

    }

    public function getUsersInClassroom(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'classroom_id' => 'required|exists:classrooms,id'
            ],
            [
                'classroom_id.required' => 'classroom_id.required',
                'classroom_id.exists' => 'classroom_id.exists',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $users = User::select('users.*')
        ->join('study_classrooms', 'users.id', '=', 'study_classrooms.user_id')
        ->where('study_classrooms.classroom_id', $request->classroom_id)
        ->orderBy('users.updated_at', 'desc')
        ->paginate(50);

        return response()->json(['data' => $users], 200, [], JSON_UNESCAPED_UNICODE);
    }
}