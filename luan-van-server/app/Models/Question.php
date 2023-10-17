<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use App\Models\Answer;

class Question extends Model
{
    use HasFactory;
    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'content',
        'question_collection_id',
        'question_status_id'
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'question_status_id' => "1",
    ];
    
    public function answersInRandomOrder()
    {
        return $this->hasMany(Answer::class, "question_id")->inRandomOrder();
    }
    public function answers()
    {
        return $this->hasMany(Answer::class, "question_id");
    }
    public function answersTrue()
    {
        return $this->hasMany(Answer::class, "question_id")->orderByDesc("is_correct");
    }
    public function questionCollection()
    {
        return $this->belongsTo(QuestionCollection::class, "question_collection_id");
    }
}
