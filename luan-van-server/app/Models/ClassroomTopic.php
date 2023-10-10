<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\Storage;

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
        'title',
        'content',
    ];
    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'title' => "",
    ];

    /**
     * The accessors to append to the model's array form.
     *
     * @var array
     */
    protected $appends = [
        'image_path'
    ];

    public function classroom()
    {
        return $this->belongsTo(Classroom::class, "classroom_id");
    }
    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
    public function comments()
    {
        return $this->hasMany(TopicComment::class, "classroom_topic_id")->where("topic_comment_status_id", 1);
    }

    protected function getImagePathAttribute()
    {
        if (Storage::disk('public')->exists('topics/' . $this->id . ".png")) {
            return Storage::url('topics/' . $this->id . ".png");
        } else {
            return "";
        }
    }
}
