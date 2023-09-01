<?php

namespace App\Http\Controllers;

use App\Models\Classroom;
use App\Models\Question;
use App\Models\Answer;
use App\Http\Requests\StoreAnswerRequest;
use App\Http\Requests\UpdateAnswerRequest;

class AnswerController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
        $classroom = Classroom::find(2);
        $questions = Question::where('classroom_id', '2')->inRandomOrder()->limit(5)->get();
        foreach ($questions as $question) {
            $question->answers;
        }
        return response()->json(['questions' => $questions], 200, [], JSON_UNESCAPED_UNICODE);
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
    public function store(StoreAnswerRequest $request)
    {
        //
        // return Answer::create($request->all());
        Answer::create([
            'content' => $request->input("content"),
        ]);
        return $request->input("content");
        // return "Hello world";
    }

    /**
     * Display the specified resource.
     */
    public function show(Answer $answer)
    {
        //
        // return $answer;
        $data = $answer;
        return response()->json($data, 200, [], JSON_UNESCAPED_UNICODE);

    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Answer $answer)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateAnswerRequest $request, Answer $answer)
    {
        //
        return $answer->update($request->all());
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Answer $answer)
    {
        //
        $answer->delete();
    }
}
