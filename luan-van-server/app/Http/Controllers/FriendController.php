<?php

namespace App\Http\Controllers;

use App\Models\Friend;
use App\Http\Requests\StoreFriendRequest;
use App\Http\Requests\UpdateFriendRequest;
use App\Models\User;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class FriendController extends Controller
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
    public function store(StoreFriendRequest $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(Friend $friend)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit(Friend $friend)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(UpdateFriendRequest $request, Friend $friend)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(Friend $friend)
    {
        //
    }

    /// kiểm tra xem người khác có phải là bạn hay chưa.
    /// Friend Status ID:
    /// 1 - 1: chưa phải là bạn, đã từng là bạn nhưng unfriend
    /// 2 - 2: đã là bạn
    /// 2 - 3: đang chờ bạn chấp nhận (mình muốn kết bạn (2) và chờ người khác (3))
    /// 3 - 2: đang chờ người khác chấp nhận (người khác muốn kết bạn(2) và chờ mình (3))


    /**
     * Đã là bạn
     * 
     * @param Request $request
     * 
     * @return [type]
     */
    public function getAcceptedFriends(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required',
            ],
            [
                'user_id.required' => 'phải có user_id',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        // kiểm tra những người đã gửi request
        $friends = Friend::where("user_id", $request->user_id)->where("friend_status_id", "2")->get();

        if (!isset($friends)) {
            return response()->json(['data' => "Không có bạn bè nào"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $friendsWithConditions = [];
        $data = [];

        foreach ($friends as $friend) {
            $dataQuery = Friend::where("user_id", $friend->other_id)
                ->where("other_id", $request->user_id)
                ->where("friend_status_id", "2")
                ->where("block", "0")
                ->first();

            if (isset($dataQuery)) {
                $friendsWithConditions[] = $dataQuery;
            }
        }

        if (empty($friendsWithConditions)) {
            return response()->json(['data' => "Không có bạn bè nào"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        foreach ($friendsWithConditions as $friendWithConditions) {
            $dataQuery = User::find($friendWithConditions->user_id);
            if (isset($dataQuery)) {
                $data[] = $dataQuery;
            }
        }

        return response()->json(['data' => $data], 200, [], JSON_UNESCAPED_UNICODE);
    }

    /**
     * đang chờ bạn chấp nhận
     * 
     * @param Request $request
     * 
     * @return [type]
     */
    public function getWaitingFriends(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required',
            ],
            [
                'user_id.required' => 'phải có user_id',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }
        // kiểm tra những người đã gửi request, có nghĩa mình là *người chờ* của người đang chờ (người khác đang chờ mình, nên là 3)
        $friends = Friend::where("user_id", $request->user_id)->where("friend_status_id", "3")->get();

        if (!isset($friends)) {
            return response()->json(['data' => "Không có bạn bè nào"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $friendsWithConditions = [];
        $data = [];

        foreach ($friends as $friend) {
            $dataQuery = Friend::where("user_id", $friend->other_id)
                ->where("other_id", $request->user_id)
                ->where("friend_status_id", "2")
                ->where("block", "0")
                ->first();

            if (isset($dataQuery)) {
                $friendsWithConditions[] = $dataQuery;
            }
        }

        if (empty($friendsWithConditions)) {
            return response()->json(['data' => "Không có bạn bè nào"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        foreach ($friendsWithConditions as $friendWithConditions) {
            $dataQuery = User::find($friendWithConditions->user_id);
            if (isset($dataQuery)) {
                $data[] = $dataQuery;
            }
        }

        return response()->json(['data' => $data], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function updateFriendStatus(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required',
                'other_id' => 'required',
                'status' => 'required',
            ],
            [
                'user_id.required' => 'phải có user_id',
                'other_id.required' => 'phải có user_id',
                'status.required' => 'phải có user_id',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $user = Friend::updateOrCreate(
            [
                'user_id' => $request->user_id,
                'other_id' => $request->other_id,
            ],
            [
                'friend_status_id' => $request->status,
            ]
        );

        $other = Friend::where("user_id", $request->other_id)->where("other_id", $request->user_id)->first();

        if (!isset($other)) {
            $other = Friend::updateOrCreate(
                [
                    'user_id' => $request->other_id,
                    'other_id' => $request->user_id,
                ],
                [
                    'friend_status_id' => "1",
                ]
            );
        }

        /** */
        if ($request->status == "1") {
            $other->friend_status_id = 1;
            $other->save();
        }

        /** */
        if ($request->status == "2") {
            if ($other->friend_status_id == 1) {
                $other->friend_status_id = 3;
                $other->save();
            }
        }
        return response()->json(['self_to_other' => $user, 'other_to_self' => $other], 200, [], JSON_UNESCAPED_UNICODE);

    }
}