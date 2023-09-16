<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class ClassroomTopic extends Model
{
    use HasFactory;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'classroom_id',
        'user_id',
        'topic_status_id',
        'content',
    ];

    public function classroom()
    {
        return $this->belongsTo(Classroom::class, "classroom_id");
    }
    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
}
