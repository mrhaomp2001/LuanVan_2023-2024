<?php

namespace App\Http\Controllers;

use App\Models\Report;
use App\Http\Requests\StoreReportRequest;
use App\Http\Requests\UpdateReportRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class ReportController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
        $reports = Report::where("model_type", "post")->paginate(7);
        $commentReports = Report::where("model_type", "comment")->paginate(7);
        return view("reports.posts.index")->with("reports", $reports)->with("commentReports", $commentReports);
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
    public function store(StoreReportRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Report $report)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Report $report)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateReportRequest $request, Report $report)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Report $report)
    {
        //
    }

    public function getReports(Request $request)
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

        $reports = Report::where("model_type", "post")->paginate(5);

        foreach ($reports as $report) {
            $report->user;
            $report->model;
            $report->report_type;
        }

        return response()->json(['data' => $reports], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function createReport(StoreReportRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => "required|exists:users,id",
                'report_type_id' => "required|exists:report_types,id",
                'model_id' => "required",
                'model_type' => "required",
                'content' => "required",
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $report = Report::create(
            [
                'user_id' => $request->user_id,
                'report_type_id' => $request->report_type_id,
                'model_id' => $request->model_id,
                'model_type' => $request->model_type,
                'content' => $request->content,
            ]
        );

        return response()->json(['data' => $report], 200, [], JSON_UNESCAPED_UNICODE);
        
    }
}