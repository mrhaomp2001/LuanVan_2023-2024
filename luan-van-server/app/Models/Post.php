<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\Storage;

class Post extends Model
{
    use HasFactory;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'title',
        'content',
        'user_id',
        'post_template_id',
        'post_status_id',
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

    protected function getImagePathAttribute()
    {
        if (Storage::disk('public')->exists('posts/' . $this->id . ".png")) {
            return Storage::url('posts/' . $this->id . ".png");
        } else {
            return "";
        }
    }

    public function postLikes()
    {
        return $this->hasMany(PostLike::class, "post_id");
    }

    public function comments()
    {
        return $this->hasMany(Comment::class, "post_id");
    }
    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
    public function postTemplate()
    {
        return $this->belongsTo(PostTemplate::class, "post_template_id");
    }
}