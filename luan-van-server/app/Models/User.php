<?php

namespace App\Models;

// use Illuminate\Contracts\Auth\MustVerifyEmail;
use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Foundation\Auth\User as Authenticatable;
use Illuminate\Notifications\Notifiable;
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
    
    public function posts()
    {
        return $this->hasMany(Post::class, "post_id");
    }

    public function comments() {
        return $this->hasMany(Comment::class, "comment_id");
    }

    public function postLikes() {
        return $this->hasMany(PostLike::class, "post_like_id");
    }

    public function commentLikes() {
        return $this->hasMany(CommentLike::class, "comment_like_id");
    }

    public function classrooms()
    {
        return $this->hasMany(StudyClassroom::class, "user_id");
    }
    
}
