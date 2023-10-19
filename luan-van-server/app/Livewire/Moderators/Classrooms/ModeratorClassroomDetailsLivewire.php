<?php

namespace App\Livewire\Moderators\Classrooms;

use App\Models\Classroom;
use App\Models\StudyDocument;
use Livewire\Component;

class ModeratorClassroomDetailsLivewire extends Component
{
    public $id;
    public $name;
    public $description;
    public $theme_color;
    public $is_open;
    public $image_path;
    public $questionCollections;

    public $classroom;

    public function mount($id)
    {
        $this->classroom = Classroom::findOrFail($id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }

        $this->id = $id;
        $this->name = $this->classroom->name;
        $this->description = $this->classroom->description;
        $this->theme_color = $this->classroom->theme_color;
        $this->is_open = $this->classroom->is_open;
        $this->image_path = $this->classroom->image_path;

        $this->questionCollections = $this->classroom->questionCollections;
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.moderator-classroom-details-livewire', [
            'documents' => StudyDocument::where('classroom_id', $this->classroom->id)->orderBy('page')->paginate(7, pageName: 'documents-page')
        ]);
    }
}