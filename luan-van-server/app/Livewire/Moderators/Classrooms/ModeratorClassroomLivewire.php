<?php

namespace App\Livewire\Moderators\Classrooms;

use App\Models\Classroom;
use Livewire\Component;

class ModeratorClassroomLivewire extends Component
{
    public $classrooms;

    public function mount()
    {
        $this->classrooms = Classroom::where("user_id", auth()->user()->id)->orderByDesc("created_at")->get();
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.moderator-classroom-livewire');
    }
}