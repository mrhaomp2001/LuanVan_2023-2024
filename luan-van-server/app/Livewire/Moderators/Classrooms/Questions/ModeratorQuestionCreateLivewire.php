<?php

namespace App\Livewire\Moderators\Classrooms\Questions;

use App\Models\Answer;
use App\Models\Classroom;
use App\Models\Question;
use App\Models\QuestionCollection;
use Livewire\Component;

class ModeratorQuestionCreateLivewire extends Component
{
    public $classroom;
    public $questionCollection;

    public $questionContent;

    public $answers = [];

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
        $this->questionContent = "";

        for ($i = 0; $i <= 4; $i++) {
            $this->answers[] = "";
        }
    }

    public function save()
    {
        $question = Question::create([
            'content' => $this->questionContent,
            'question_collection_id' => $this->questionCollection->id,
        ]);

        Answer::create([
            'content' => $this->answers[0],
            'is_correct' => true,
            'question_id' => $question->id,
        ]);

        for ($i = 1; $i < 4; $i++) {
            Answer::create([
                'content' => $this->answers[$i],
                'is_correct' => false,
                'question_id' => $question->id,
            ]);
        }

        return $this->redirect(route("moderator.question-collections.show", ["classroom_id" => $this->classroom->id, "question_collection_id" => $this->questionCollection->id]), true);
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.questions.moderator-question-create-livewire');
    }
}