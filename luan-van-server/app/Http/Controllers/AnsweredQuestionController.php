<?php

namespace App\Http\Controllers;

use App\Models\AnsweredQuestion;
use App\Http\Requests\StoreAnsweredQuestionRequest;
use App\Http\Requests\UpdateAnsweredQuestionRequest;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Carbon;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Validator;

class AnsweredQuestionController extends Controller
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
    public function store(StoreAnsweredQuestionRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(AnsweredQuestion $answeredQuestion)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(AnsweredQuestion $answeredQuestion)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAnsweredQuestionRequest $request, AnsweredQuestion $answeredQuestion)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(AnsweredQuestion $answeredQuestion)
    {
        //
    }

    public function answeredQuestion(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => "required|exists:users,id",
                'data' => "required|array",
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        for ($i = 0; $i < count($request->input('data')); $i++) {
            AnsweredQuestion::create([
                'user_id' => $request->user_id,
                'answer_id' => $request->data[$i]
            ]);
        }
        return response()->json(['data' => "Gủi kết quả thành công", 'result_test' => $request->input("data")], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getRanksDay(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [

            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $currentDate = Carbon::now();

        $rankings = DB::table('answered_questions')
            ->select(['user_id', DB::raw('count(*) as total_answers')])
            ->whereDate('created_at', $currentDate)
            ->groupBy('user_id')
            ->orderByDesc('total_answers')
            ->get();

        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);

    }
    public function getRanksWeek(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [

            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $currentDateStart = Carbon::now();
        $currentDateEnd = Carbon::now();

        $rankings = DB::table('answered_questions')
            ->select('user_id', DB::raw('count(*) as total_answers'))
            ->whereBetween('created_at', [$currentDateStart->startOfWeek(), $currentDateEnd->endOfWeek()])
            ->groupBy('user_id')
            ->orderByDesc('total_answers')
            ->get();

        return response()->json([
            'data' => $rankings
        ], 200, [], JSON_UNESCAPED_UNICODE);

    }

    public function getRanksMonth(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [

            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $currentDateYear = Carbon::now();
        $currentDateMonth = Carbon::now();

        $rankings = DB::table('answered_questions')
            ->select(['user_id', DB::raw('count(*) as total_answers')])
            ->whereYear('created_at', $currentDateYear->year)
            ->whereMonth('created_at', $currentDateMonth->month)
            ->groupBy('user_id')
            ->orderByDesc('total_answers')
            ->get();

        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);

    }

    public function getRanksDayQuestionCollection(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [

            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        $questionCollectionId = 2;
        $currentDate = Carbon::now();

        $rankings = DB::table('answered_questions')
            ->join('answers', 'answered_questions.answer_id', '=', 'answers.id')
            ->join('questions', 'answers.question_id', '=', 'questions.id')
            ->select('answered_questions.user_id', DB::raw('count(*) as total_answers'))
            ->where('questions.question_collection_id', $questionCollectionId)
            ->whereDate('answered_questions.created_at', $currentDate)
            ->groupBy('answered_questions.user_id')
            ->orderByDesc('total_answers')
            ->get();

        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);

    }

    public function getRanksDayClassroom(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'classroom_id' => 'required|exists:classrooms,id'
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        $currentDate = Carbon::now();

        // Truy vấn để lấy bảng xếp hạng người dùng dựa trên số câu hỏi đã trả lời từ classroom cụ thể
        $rankings = DB::table('answered_questions')
            ->join('answers', 'answered_questions.answer_id', '=', 'answers.id')
            ->join('questions', 'answers.question_id', '=', 'questions.id')
            ->join('question_collections', 'questions.question_collection_id', '=', 'question_collections.id')
            ->select('answered_questions.user_id', DB::raw('count(*) as total_answers'))
            ->where('question_collections.classroom_id', $request->classroom_id)
            ->where('answers.is_correct', "1")
            ->whereDate('answered_questions.created_at', $currentDate)
            ->groupBy('answered_questions.user_id')
            ->orderByDesc('total_answers')
            ->get();

        foreach ($rankings as $ranking) {
            $ranking->user = User::find($ranking->user_id);
        }
        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getRanksWeekClassroom(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'classroom_id' => 'required|exists:classrooms,id'
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        $currentDateStart = Carbon::now();
        $currentDateEnd = Carbon::now();

        // Truy vấn để lấy bảng xếp hạng người dùng dựa trên số câu hỏi đã trả lời từ classroom cụ thể
        $rankings = DB::table('answered_questions')
            ->join('answers', 'answered_questions.answer_id', '=', 'answers.id')
            ->join('questions', 'answers.question_id', '=', 'questions.id')
            ->join('question_collections', 'questions.question_collection_id', '=', 'question_collections.id')
            ->select('answered_questions.user_id', DB::raw('count(*) as total_answers'))
            ->where('answers.is_correct', "1")
            ->where('question_collections.classroom_id', $request->classroom_id)
            ->whereBetween('answered_questions.created_at', [$currentDateStart->startOfWeek(), $currentDateEnd->endOfWeek()])
            ->groupBy('answered_questions.user_id')
            ->orderByDesc('total_answers')
            ->get();

        foreach ($rankings as $ranking) {
            $ranking->user = User::find($ranking->user_id);
        }
        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getRanksMonthClassroom(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'classroom_id' => 'required|exists:classrooms,id'
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        $currentDateYear = Carbon::now();
        $currentDateMonth = Carbon::now();

        // Truy vấn để lấy bảng xếp hạng người dùng dựa trên số câu hỏi đã trả lời từ classroom cụ thể
        $rankings = DB::table('answered_questions')
            ->join('answers', 'answered_questions.answer_id', '=', 'answers.id')
            ->join('questions', 'answers.question_id', '=', 'questions.id')
            ->join('question_collections', 'questions.question_collection_id', '=', 'question_collections.id')
            ->select('answered_questions.user_id', DB::raw('count(*) as total_answers'))
            ->where('question_collections.classroom_id', $request->classroom_id)
            ->where('answers.is_correct', "1")
            ->whereYear('answered_questions.created_at', $currentDateYear->year)
            ->whereMonth('answered_questions.created_at', $currentDateMonth->month)
            ->groupBy('answered_questions.user_id')
            ->orderByDesc('total_answers')
            ->get();

        foreach ($rankings as $ranking) {
            $ranking->user = User::find($ranking->user_id);
        }
        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);
    }
}