<?php

namespace App\Livewire\Moderators\Classrooms;

use App\Models\Classroom;
use Illuminate\Support\Facades\Storage;
use Livewire\Component;
use Livewire\WithFileUploads;
use Livewire\Attributes\Rule;

class ModeratorClassroomCreateLivewire extends Component
{
    use WithFileUploads;

    public $name;
    public $description;
    public $theme_color = "#ffffff";
    public $image;

    public function mount()
    {
    }

    public function save()
    {
        $validated = $this->validate([ 
            'name' => 'required|min:3',
            'description' => 'required|min:3',
        ]);

        $classroom = Classroom::create([
            'name' => $this->name,
            'description' => $this->description,
            'theme_color' => $this->theme_color,
            'user_id' => auth()->user()->id,
            'is_open' => false,
        ]);

        if (isset($this->image)) {
            Storage::disk('public')->putFileAs("classrooms/avatars", $this->image, $classroom->id . '.png');
        }

        return $this->redirect(route("moderator.classrooms.index"), navigate: true);
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.moderator-classroom-create-livewire');
    }
}