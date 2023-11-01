<?php

namespace App\Http\Controllers;

use App\Models\Message;
use App\Http\Requests\StoreMessageRequest;
use App\Http\Requests\UpdateMessageRequest;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\DB;
use Illuminate\Support\Facades\Validator;

class MessageController extends Controller
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
    public function store(StoreMessageRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Message $message)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Message $message)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateMessageRequest $request, Message $message)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Message $message)
    {
        //
    }

    public function getMessagesFirstLogin(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => ["required", "exists:users,id"],
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $user_id = $request->user_id;

        $messages = DB::table('messages')
            ->select(DB::raw('MAX(created_at) as latest_message_date'))
            ->where('sender_id', $user_id)
            ->orWhere('receiver_id', $user_id)
            ->groupBy(DB::raw('IF(sender_id = ' . $user_id . ', receiver_id, sender_id)'))
            ->get();

        $messageIds = [];

        foreach ($messages as $message) {
            $messageIds[] = $message->latest_message_date;
        }

        $latestMessages = Message::whereIn('created_at', $messageIds)
            ->orderByDesc("created_at")
            ->get();

        return response()->json(['data' => $latestMessages], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function getMessages(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'sender_id' => ["required", "exists:users,id"],
                'receiver_id' => ["required", "exists:users,id"],
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $messages = Message::where("sender_id", $request->sender_id)
            ->orWhere("receiver_id", $request->sender_id)
            ->where("sender_id", $request->receiver_id)
            ->where("receiver_id", $request->receiver_id)
            ->orderByDesc("created_at")
            ->paginate(10);

        return response()->json(['data' => $messages], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function sendMessage(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'sender_id' => ["required", "exists:users,id"],
                'receiver_id' => ["required", "exists:users,id"],
                'content' => ['required'],
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $message = Message::create([
            'sender_id' => $request->sender_id,
            'receiver_id' => $request->receiver_id,
            'content' => $request->content,
        ]);

        return response()->json(['data' => $message], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
