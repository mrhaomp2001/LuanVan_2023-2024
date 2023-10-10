<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class TopicComment extends Model
{
    use HasFactory;
    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'user_id',
        'classroom_topic_id',
        'topic_comment_status_id',
        'content',
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'topic_comment_status_id' => 1,
    ];

    public function topic()
    {
        return $this->belongsTo(ClassroomTopic::class, "classroom_topic_id");
    }

    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
}