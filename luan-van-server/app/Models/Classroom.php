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
    ];

    public function questionCollections()
    {
        return $this->hasMany(QuestionCollection::class, "classroom_id");
    }

    public function users()
    {
        return $this->hasMany(StudyClassroom::class, "classroom_id");
    }
    public function studyDocuments()
    {
        return $this->hasMany(StudyDocument::class, "classroom_id");
    }
}
