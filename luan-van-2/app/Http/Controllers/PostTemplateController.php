<?php

namespace App\Http\Controllers;

use App\Models\PostTemplate;
use App\Http\Requests\StorePostTemplateRequest;
use App\Http\Requests\UpdatePostTemplateRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class PostTemplateController extends Controller
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
    public function store(StorePostTemplateRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(PostTemplate $postTemplate)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(PostTemplate $postTemplate)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdatePostTemplateRequest $request, PostTemplate $postTemplate)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(PostTemplate $postTemplate)
    {
        //
    }

    public function indexApi(Request $request)
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
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $postTemplates = PostTemplate::all();

        return response()->json(['data' => $postTemplates], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function storeApi(StorePostTemplateRequest $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'name' => "required",
                'content' => "required"
            ],
            [
                'name.required' => "Cần phải có tên của mẫu bài đăng",
                'content.required' => "Cần phải có quy định của mẫu bài đăng",
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $postTemplate = PostTemplate::create([
            'name' => $request->name,
            'content' => $request->content,
        ]);

        return response()->json(['data' => $postTemplate], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
