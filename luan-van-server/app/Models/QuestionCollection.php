<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class QuestionCollection extends Model
{
    use HasFactory;

    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'name',
        'difficulty',
        'classroom_id',
        'game_id',
        'questions_per_time',
        'is_open'
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'game_id' => "1",
        'is_open' => false,
    ];

    /**
     * The accessors to append to the model's array form.
     *
     * @var array
     */
    protected $appends = [
        'game'
    ];
    
    /**
     * The attributes that should be cast.
     *
     * @var array<string, string>
     */
    protected $casts = [
        'is_open' => 'boolean',
    ];

    protected function getGameAttribute()
    {
        return Game::find($this->game_id);
    }

    public function questions()
    {
        return $this->hasMany(Question::class, "question_collection_id")->where("question_status_id", 1);
    }

    public function questionsDeleted()
    {
        return $this->hasMany(Question::class, "question_collection_id")->where("question_status_id", 2);
    }
}