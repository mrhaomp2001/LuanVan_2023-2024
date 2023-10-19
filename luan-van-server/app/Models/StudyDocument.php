<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\Storage;

class StudyDocument extends Model
{
    use HasFactory;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'classroom_id',
        'content',
        'page',
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
        if (Storage::disk('public')->exists('documents/' . $this->id . ".png")) {
            return Storage::disk('public')->url('documents/' . $this->id . ".png");
        } else {
            return "";
        }
    }

    public function classroom()
    {
        return $this->belongsTo(Classroom::class, "classroom_id");
    }
}
