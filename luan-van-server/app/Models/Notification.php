<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Notification extends Model
{
    use HasFactory;


    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'user_id',
        'sender_id',
        'model_id',
        'notification_type_id',
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'sender_id' => "",
        'model_id' => "",
    ];

    /**
     * The accessors to append to the model's array form.
     *
     * @var array
     */
    protected $appends = [
        'model',
        'notification_type',
        'sender'
    ];

    protected function getNotificationTypeAttribute()
    {
        return NotificationType::find($this->notification_type_id);
    }

    protected function getSenderAttribute()
    {
        if ($this->sender_id != "") {
            return User::find($this->sender_id);
        }
        return "Gửi từ server";
    }

    protected function getModelAttribute()
    {
        if ($this->notification_type_id == 1 || $this->notification_type_id == 2) {
            $post = Post::find($this->model_id);

            if ($post->post_status_id == 1) {
                return $post;
            }

            $post->error = "Đây là bài viết đã xóa hoặc riêng tư! Không được hiển thị";
            return $post;
        }

        if ($this->notification_type_id == 3) {
            $comment = Comment::find($this->model_id);

            if ($comment->comment_status_id == 1) {
                return $comment;
            }

            $comment->error = "Đây là bình luận đã xóa! Không được hiển thị";
            return $comment;
        }

        return "";
    }
    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
}