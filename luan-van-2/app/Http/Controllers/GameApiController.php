<?php

namespace App\Http\Controllers;

use App\Http\Requests\Auth\LoginRequest;
use App\Models\Classroom;
use App\Models\Post;
use App\Models\User;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;
use App\Models\Question;
use Illuminate\Http\Request;

class GameApiController extends Controller
{
    public function getQuestions(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'class' => 'required',
                'amount' => 'required'
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $questions = Question::where('classroom_id', $request->class)->inRandomOrder()->limit($request->amount)->get();
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
            'classroom_id' => "1",
        ]);

        return response()->json(['message' => "tạo tài khoản thành công"], 200, [], JSON_UNESCAPED_UNICODE);
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

        $user->classroom;

        $user->posts;

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
    public function getClassrooms(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'per_page' => 'required'
            ],
            [
                'per_page.required' => 'phải có số phần tử trên một trang cụ thể',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $classrooms = Classroom::simplePaginate($request->per_page);

        foreach ($classrooms as $classroom) {

            if (Storage::disk('public')->exists('classrooms/avatars/' . $classroom->id . ".png")) {
                $classroom->avatar_path = Storage::url('classrooms/avatars/' . $classroom->id . ".png");
            }
            else {
                $classroom->avatar_path = "";
            }
        }

        return response()->json(['data' => $classrooms], 200, [], JSON_UNESCAPED_UNICODE);
    }

}