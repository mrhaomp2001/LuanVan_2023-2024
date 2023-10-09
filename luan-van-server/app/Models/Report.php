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
        'content',
    ];
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
    }

    public function reportType() {
        return $this->belongsTo(ReportType::class, "report_type_id");
    }
}