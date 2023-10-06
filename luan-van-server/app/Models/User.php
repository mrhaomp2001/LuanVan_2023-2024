<?php

namespace App\Models;

// use Illuminate\Contracts\Auth\MustVerifyEmail;
use Illuminate\Database\Eloquent\Casts\Attribute;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Illuminate\Notifications\Notifiable;
use Illuminate\Support\Facades\Storage;
use Laravel\Sanctum\HasApiTokens;

class User extends Authenticatable
{
    use HasApiTokens, HasFactory, Notifiable;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'name',
        'username',
        'password',
        'classroom_id'
    ];

    /**
     * The attributes that should be hidden for serialization.
     *
     * @var array<int, string>
     */
    protected $hidden = [
        'remember_token',
        'password',
    ];

    // /**
    //  * The attributes that should be cast.
    //  *
    //  * @var array<string, string>
    //  */
    protected $casts = [
        'email_verified_at' => 'datetime',
        'password' => 'hashed',
    ];

    /**
     * The accessors to append to the model's array form.
     *
     * @var array
     */
    protected $appends = [
        'avatar_path'
    ];

    protected function getAvatarPathAttribute()
    {
        if (Storage::disk('public')->exists('users/avatars/' . $this->id . ".png")) {
            return Storage::url('users/avatars/' . $this->id . ".png");
        } else {
            return "";
        }
    }
    
    public function posts()
    {
        return $this->hasMany(Post::class, "user_id");
    }

    public function comments() {
        return $this->hasMany(Comment::class, "user_id");
    }

    public function postLikes() {
        return $this->hasMany(PostLike::class, "user_id");
    }

    public function commentLikes() {
        return $this->hasMany(CommentLike::class, "user_id");
    }

    public function classrooms()
    {
        return $this->hasMany(StudyClassroom::class, "user_id");
    }

    public function notifications()
    {
        return $this->hasMany(Notification::class, "user_id")->orderByDesc("created_at");
    }
}
