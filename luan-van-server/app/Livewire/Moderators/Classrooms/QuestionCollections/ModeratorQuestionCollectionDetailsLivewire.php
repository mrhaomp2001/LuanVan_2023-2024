<?php

namespace App\Livewire\Moderators\Classrooms\QuestionCollections;

use App\Models\Classroom;
use App\Models\QuestionCollection;
use Livewire\Component;

class ModeratorQuestionCollectionDetailsLivewire extends Component
{
    public $classroom;
    public $questionCollection;

    public function mount($classroom_id, $question_collection_id)
    {
        $this->classroom = Classroom::findOrFail($classroom_id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }

        $this->questionCollection = QuestionCollection::findOrFail($question_collection_id);

        if ($this->questionCollection->classroom_id != $this->classroom->id) {
            return redirect(route("404"));
        }
    }
    public function render()
    {
        return view('livewire.moderators.classrooms.question-collections.moderator-question-collection-details-livewire');
    }
}