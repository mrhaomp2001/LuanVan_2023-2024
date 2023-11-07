<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Report extends Model
{
    use HasFactory;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'user_id',
        'report_type_id',
        'model_id',
        'model_type',
        'report_response_id',
        'content',
        'responder'
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'report_response_id' => "1",
        'content' => "",
    ];

    /**
     * The accessors to append to the model's array form.
     *
     * @var array
     */
    protected $appends = [
        'report_response'
    ];

    protected function getReportResponseAttribute()
    {
        return ReportRespone::find($this->report_response_id);
    }

    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }

    public function model()
    {
        if ($this->model_type == "post") {
            return $this->belongsTo(Post::class, "model_id");
        }

        if ($this->model_type == "comment") {
            return $this->belongsTo(Comment::class, "model_id");
        }

        if ($this->model_type == "topic") {
            return $this->belongsTo(ClassroomTopic::class, "model_id");
        }

        if ($this->model_type == "topic_comment") {
            return $this->belongsTo(TopicComment::class, "model_id");
        }
    }

    public function reportType() {
        return $this->belongsTo(ReportType::class, "report_type_id");
    }
}