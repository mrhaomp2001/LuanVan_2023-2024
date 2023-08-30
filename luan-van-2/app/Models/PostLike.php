<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class PostLike extends Model
{
    use HasFactory;
    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'user_id',
        'post_id',
        'like_status',
    ];

    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
    public function post()
    {
        return $this->belongsTo(Post::class, "post_id");
    }
}