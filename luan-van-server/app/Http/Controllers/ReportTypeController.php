<?php

namespace App\Http\Controllers;

use App\Models\ReportType;
use App\Http\Requests\StoreReportTypeRequest;
use App\Http\Requests\UpdateReportTypeRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class ReportTypeController extends Controller
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
    public function store(StoreReportTypeRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(ReportType $reportType)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(ReportType $reportType)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateReportTypeRequest $request, ReportType $reportType)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(ReportType $reportType)
    {
        //
    }
    public function getReportPostsTypes(Request $request) {
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

        $reports = ReportType::where("model_type", "post")->paginate(5);

        return response()->json(['data' => $reports], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getReportCommentsTypes(Request $request) {
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

        $reports = ReportType::where("model_type", "comment")->paginate(5);

        return response()->json(['data' => $reports], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
