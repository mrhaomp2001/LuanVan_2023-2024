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
    ];
    
    public function answersInRandomOrder()
    {
        return $this->hasMany(Answer::class, "question_id")->inRandomOrder();
    }
    public function answers()
    {
        return $this->hasMany(Answer::class, "question_id");
    }
    public function questionCollection()
    {
        return $this->belongsTo(QuestionCollection::class, "question_collection_id");
    }
}
