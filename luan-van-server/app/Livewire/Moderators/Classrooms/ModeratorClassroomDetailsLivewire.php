<?php

namespace App\Livewire\Moderators\Classrooms;

use App\Models\Classroom;
use Livewire\Component;

class ModeratorClassroomDetailsLivewire extends Component
{
    public $id;
    public $name;
    public $description;
    public $theme_color;
    public $is_open;
    public $image_path;

    public function mount($id)
    {
        $classroom = Classroom::findOrFail($id);

        if ($classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }

        $this->id = $id;
        $this->name = $classroom->name;
        $this->description = $classroom->description;
        $this->theme_color = $classroom->theme_color;
        $this->is_open = $classroom->is_open;
        $this->image_path = $classroom->image_path;
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.moderator-classroom-details-livewire');
    }
}