<?php

namespace App\Http\Controllers;

use App\Models\Message;
use App\Http\Requests\StoreMessageRequest;
use App\Http\Requests\UpdateMessageRequest;
use App\Models\User;
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

        foreach ($latestMessages as $latestMessage) {
            $latestMessage->other_user = User::find(($latestMessage->sender_id == $request->user_id) ? $latestMessage->receiver_id : $latestMessage->sender_id);
        }

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

        $senderId = $request->sender_id;
        $receiverId = $request->receiver_id;

        $messages = Message::where(function ($query) use ($senderId, $receiverId) {
            $query->where('sender_id', $senderId)
                ->where('receiver_id', $receiverId);
        })->orWhere(function ($query) use ($senderId, $receiverId) {
            $query->where('sender_id', $receiverId)
                ->where('receiver_id', $senderId);
        })->orderBy('created_at', 'desc')->get();

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

        $message->sender;
        $message->receiver;

        return response()->json(['data' => $message], 200, [], JSON_UNESCAPED_UNICODE);
    }
}
