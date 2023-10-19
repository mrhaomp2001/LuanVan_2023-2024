<?php

namespace App\Livewire\Moderators\Classrooms\Documents;

use App\Models\Classroom;
use App\Models\StudyDocument;
use Illuminate\Support\Facades\Storage;
use Livewire\Component;
use Livewire\WithFileUploads;

class ModeratorDocumentCreateLivewire extends Component
{
    use WithFileUploads;

    public $classroom;
    public $image;

    public $content;

    public function mount($classroom_id)
    {
        $this->classroom = Classroom::findOrFail($classroom_id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }


    }

    public function save()
    {
        $validated = $this->validate(
            [
                'content' => 'required|min:3',
                'image' => ['required']
            ],
            [

            ]
        );

        $document = StudyDocument::create([
            'content' => $this->content,
            'classroom_id' => $this->classroom->id,
            'page' => (count($this->classroom->studyDocuments) + 1),
        ]);

        if (isset($this->image)) {
            Storage::disk('public')->putFileAs("documents", $this->image, $document->id . '.png');
        }
        return  $this->redirect(route("moderator.classrooms.show", ['id' => $this->classroom->id]));

    }


    public function render()
    {
        return view('livewire.moderators.classrooms.documents.moderator-document-create-livewire');
    }
}