<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Post extends Model
{
    use HasFactory;

        /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'content',
        'user_id',
        'post_template_id',
    ];

    public function post_likes()
    {
        return $this->hasMany(PostLike::class, "post_id");
    }

    public function post_likes_up()
    {
        return $this->hasMany(PostLike::class, "post_id")->where("like_status", "1");
    }

    public function post_likes_down()
    {
        return $this->hasMany(PostLike::class, "post_id")->where("like_status", "-1");
    }

    public function comments()
    {
        return $this->hasMany(Comment::class, "post_id");
    }
    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
    public function post_template()
    {
        return $this->belongsTo(PostTemplate::class, "post_template_id");
    }
}
