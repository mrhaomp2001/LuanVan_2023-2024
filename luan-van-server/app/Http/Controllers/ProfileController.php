<?php

namespace App\Http\Controllers;

use App\Http\Requests\ProfileUpdateRequest;
use App\Models\Friend;
use App\Models\User;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Redirect;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;
use Illuminate\View\View;
use App\Models\Answer;
use App\Models\Question;

class ProfileController extends Controller
{
    /**
     * Display the user's profile form.
     */
    public function edit(Request $request): View
    {
        $url = "1";

        return view('profile.edit', [
            'user' => $request->user(),
            'data' => $url,
        ]);
    }

    /**
     * Update the user's profile information.
     */
    public function update(ProfileUpdateRequest $request): RedirectResponse
    {
        $request->user()->fill($request->validated());

        if ($request->user()->isDirty('email')) {
            $request->user()->email_verified_at = null;
        }

        $request->user()->save();

        return Redirect::route('profile.edit')->with('status', 'profile-updated');
    }

    /**
     * Delete the user's account.
     */
    public function destroy(Request $request): RedirectResponse
    {
        $request->validateWithBag('userDeletion', [
            'password' => ['required', 'current_password'],
        ]);

        $user = $request->user();

        Auth::logout();

        $user->delete();

        $request->session()->invalidate();
        $request->session()->regenerateToken();

        return Redirect::to('/');
    }

    public function getUserInfomationsApi(Request $request) {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:user,id',
                'profile_user_id' => 'required|exists:user,id',
            ],
            [
                'user_id.required' => 'User Id không được rỗng',
                'profile_user_id.required' => 'Profile User Id không được rỗng',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $user = User::find($request->profile_user_id);
        
        $friendToOther = Friend::where("user_id", $request->user_id)->where("other_id", $request->profile_user_id)->first(); 
        $friendToUser = Friend::where("user_id", $request->profile_user_id)->where("other_id", $request->user_id)->first();

        $user->friend_to_other = $friendToOther;
        $user->friend_to_user = $friendToUser;

        if (isset($user)) {
            return response()->json(['data' => $user], 200, [], JSON_UNESCAPED_UNICODE);
        }
        else { 
            return response()->json(['data' => "Không tìm được user"], 200, [], JSON_UNESCAPED_UNICODE);
        }

    }
}