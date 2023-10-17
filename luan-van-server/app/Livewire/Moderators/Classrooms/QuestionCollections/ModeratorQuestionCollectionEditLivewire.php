<?php

namespace App\Livewire\Moderators\Classrooms\QuestionCollections;

use App\Models\Classroom;
use App\Models\Game;
use App\Models\QuestionCollection;
use Livewire\Component;

class ModeratorQuestionCollectionEditLivewire extends Component
{
    public $classroom;
    public $questionCollection;
    public $games;


    public $name;
    public $difficulty;
    public $questions_per_time;
    public $game_id;

    public $is_open;

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
        $this->games = Game::all();

        $this->name = $this->questionCollection->name;


        $this->difficulty = $this->questionCollection->difficulty;
        $this->questions_per_time = $this->questionCollection->questions_per_time;
        $this->game_id = $this->questionCollection->game_id;
        $this->is_open = $this->questionCollection->is_open ? 1 : 0;
    }

    public function save()
    {
        //
        $validated = $this->validate(
            [
                'name' => 'required|min:3',
                'difficulty' => 'required|min:3',
                'questions_per_time' => 'required|numeric|min:4|max:16',
                'game_id' => "required|exists:games,id",
                'is_open' => ['required', 'boolean'],
            ],
            [

            ]
        );
        if ($this->questionCollection->classroom_id != $this->classroom->id) {
            return redirect(route("404"));
        }
        $this->questionCollection->name = $this->name;
        $this->questionCollection->difficulty = $this->difficulty;
        $this->questionCollection->game_id = $this->game_id;
        $this->questionCollection->is_open = $this->is_open;
        $this->questionCollection->questions_per_time = $this->questions_per_time;
        $this->questionCollection->save();

        return redirect(route("moderator.classrooms.show", ["id" => $this->classroom->id]));
    }
    public function render()
    {
        return view('livewire.moderators.classrooms.question-collections.moderator-question-collection-edit-livewire');
    }
}