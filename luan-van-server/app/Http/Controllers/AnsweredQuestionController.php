<?php

namespace App\Http\Controllers;

use App\Models\AnsweredQuestion;
use App\Http\Requests\StoreAnsweredQuestionRequest;
use App\Http\Requests\UpdateAnsweredQuestionRequest;
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

            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        $currentDate = Carbon::now();
        $classroomId = 2; // Điền vào ID của classroom bạn muốn xếp hạng

        // Truy vấn để lấy bảng xếp hạng người dùng dựa trên số câu hỏi đã trả lời từ classroom cụ thể
        $rankings = DB::table('answered_questions')
            ->join('answers', 'answered_questions.answer_id', '=', 'answers.id')
            ->join('questions', 'answers.question_id', '=', 'questions.id')
            ->join('question_collections', 'questions.question_collection_id', '=', 'question_collections.id')
            ->select('answered_questions.user_id', DB::raw('count(*) as total_answers'))
            ->where('question_collections.classroom_id', $classroomId)
            ->whereDate('answered_questions.created_at', $currentDate)

            ->groupBy('answered_questions.user_id')
            ->orderByDesc('total_answers')
            ->get();

        return response()->json(['data' => $rankings], 200, [], JSON_UNESCAPED_UNICODE);

    }
}