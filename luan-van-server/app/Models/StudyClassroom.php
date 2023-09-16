<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class StudyClassroom extends Model
{
    use HasFactory;

    protected $fillable = [
        'user_id',
        'classroom_id',
        'study_status_id',
    ];

    public function user()
    {
        return $this->belongsTo(User::class, "user_id");
    }
    public function classroom()
    {
        return $this->belongsTo(Classroom::class, "classroom_id");
    }
}
