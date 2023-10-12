<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Classroom extends Model
{
    use HasFactory;
    /**
     * The attributes that are mass assignable.
     *
     * @var array<int, string>
     */
    protected $fillable = [
        'name',
        'description',
        'theme_color',
        'user_id',
    ];
    /**
     * The accessors to append to the model's array form.
     *
     * @var array
     */
    protected $appends = [
        'user'
    ];

    protected $casts = [
        'is_open' => 'boolean',
    ];

    /**
     * The model's default values for attributes.
     *
     * @var array
     */
    protected $attributes = [
        'is_open' => false,
    ];

    public function questionCollections()
    {
        return $this->hasMany(QuestionCollection::class, "classroom_id");
    }

    public function users()
    {
        return $this->hasMany(StudyClassroom::class, "classroom_id")->orderBy("updated_at")->where("study_status_id", 1);
    }
    public function studyDocuments()
    {
        return $this->hasMany(StudyDocument::class, "classroom_id");
    }

    protected function getUserAttribute()
    {
        return User::find($this->user_id);
    }
}
