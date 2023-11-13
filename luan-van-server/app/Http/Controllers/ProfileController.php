<?php

namespace App\Http\Controllers;

use App\Http\Requests\ProfileUpdateRequest;
use App\Models\Friend;
use App\Models\User;
use Illuminate\Http\RedirectResponse;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Hash;
use Illuminate\Support\Facades\Redirect;
use Illuminate\Support\Facades\Storage;
use Illuminate\Support\Facades\Validator;
use Illuminate\Validation\Rules\File;
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

    public function getUserInfomationsApi(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'profile_user_id' => 'required|exists:users,id',
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
        } else {
            return response()->json(['data' => "Không tìm được user"], 200, [], JSON_UNESCAPED_UNICODE);
        }

    }

    public function updateAvatar(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'image' => [
                    'required',
                    File::image()
                        ->min(64)
                        ->max(64 * 1024)
                ]
            ],
            [

            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        if (isset($request->image)) {
            Storage::disk('public')->putFileAs("users/avatars/", $request->image, $request->user_id . '.png');
        }

        return response()->json(['message' => 'Cập nhật thành công, Thông tin đã thay đổi của bạn sẽ được hiển thị vào lần đăng nhập tới.'], 200, [], JSON_UNESCAPED_UNICODE);
    }

    public function updateNameProfile(Request $request)
    {
        $input = $request->all();
        $validator = Validator::make(
            $input,
            [
                'user_id' => 'required|exists:users,id',
                'name' => 'required|min:1|max:64',
                'password' => 'required|min:6|max:64'
            ],
            [
                'name.required' => 'Tên cần đổi không được rỗng',
                'name.min' => 'Tên cần đổi phải lớn hơn 1 ký tự',
                'name.max' => 'Tên cần đổi phải nhỏ hơn 64 ký tự',
                'password.required' => 'Mật khẩu là bắt buộc',
                'password.min' => 'Mật khẩu phải lớn hơn 6 ký tự',
                'password.max' => 'Mật khẩu phải nhỏ hơn 64 ký tự',
            ]
        );

        if ($validator->fails()) {
            return response()->json(['message' => $validator->errors()->first(), 'data' => $request->all()], 200, [], JSON_UNESCAPED_UNICODE);
        }

        $user = User::where('id', $request->user_id)->first();

        if (Hash::check($request->password, $user->password)) {
            $user->name = $request->name;
            $user->save();
            return response()->json(['message' => "Thông tin của bạn sẽ thay đổi ở lần đăng nhập kế tiếp"], 200, [], JSON_UNESCAPED_UNICODE);
        }

        return response()->json(['message' => "Sai mật khẩu"], 200, [], JSON_UNESCAPED_UNICODE);
    }
}