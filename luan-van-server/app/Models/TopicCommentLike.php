<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class TopicCommentLike extends Model
{
    use HasFactory;
    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'topic_comment_id',
        'user_id',
        'like_status',
    ];
    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }

    public function topic_comment()
    {
        return $this->belongsTo(TopicComment::class, "topic_comment_id");
    }
}