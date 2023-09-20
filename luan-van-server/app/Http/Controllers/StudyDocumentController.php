<?php

namespace App\Http\Controllers;

use App\Models\StudyDocument;
use App\Http\Requests\StoreStudyDocumentRequest;
use App\Http\Requests\UpdateStudyDocumentRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class StudyDocumentController extends Controller
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
    public function store(StoreStudyDocumentRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(StudyDocument $studyDocument)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(StudyDocument $studyDocument)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateStudyDocumentRequest $request, StudyDocument $studyDocument)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(StudyDocument $studyDocument)
    {
        //
    }

    public function getStudyDocuments(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'classroom_id' => 'required|exists:classrooms,id',

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

        $documents = StudyDocument::where("classroom_id", $request->classroom_id)
        ->orderBy("page")
        ->get();

        $count = count($documents);

        return response()->json(
            [
                'data' => $documents, 
                'count' => $count
            ], 
            200, [], JSON_UNESCAPED_UNICODE);
    }
}