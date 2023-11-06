<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class CommentLike extends Model
{
    use HasFactory;
    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'user_id',
        'comment_id',
        'like_status',
    ];

    public function comment()
    {
        return $this->belongsTo(Comment::class, "comment_id");
    }

    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
}
