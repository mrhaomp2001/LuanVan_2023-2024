<?php

namespace App\Livewire\Moderators\Classrooms\Questions;

use App\Models\Classroom;
use App\Models\Question;
use App\Models\QuestionCollection;
use Livewire\Component;

class ModeratorQuestionEditLivewire extends Component
{
    public $classroom;
    public $questionCollection;
    public $question;

    public $questionContent;
    public $answers = [];
    public function mount($classroom_id, $question_collection_id, $question_id)
    {
        $this->classroom = Classroom::findOrFail($classroom_id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }

        $this->questionCollection = QuestionCollection::findOrFail($question_collection_id);

        if ($this->questionCollection->classroom_id != $this->classroom->id) {
            return redirect(route("404"));
        }

        $this->question = Question::findOrFail($question_id);

        if ($this->question->question_collection_id != $this->questionCollection->id) {
            return redirect(route("404"));
        }

        $this->questionContent = $this->question->content;

        foreach ($this->question->answersTrue as $answer) {
            $this->answers[] = $answer->content;
        }
    }

    public function save()
    {
        $this->validate(
            [
                'questionContent' => 'required|min:1|max:512',
                'answers.*' => 'required|min:1|max:512',
            ],
            [
                'questionContent.required' => "Cần nhập câu hỏi",
                'questionContent.min' => "Cần nhập câu hỏi với tối thiểu :min ký tự",
                'questionContent.max' => "Cần nhập câu hỏi với tối đa :max ký tự",

                'answers.*.required' => "Cần nhập câu trả lời",
                'answers.*.min' => "Cần nhập câu trả lời với tối thiểu :min ký tự",
                'answers.*.max' => "Cần nhập câu trả lời với tối đa :max ký tự",
            ]
        );

        $this->question->content = $this->questionContent;
        $this->question->save();

        for ($i = 0; $i < 4; $i++) {
            $this->question->answersTrue[$i]->content = $this->answers[$i];
            $this->question->answersTrue[$i]->save();
        }
        return $this->redirect(route("moderator.question-collections.show", ["classroom_id" => $this->classroom->id, "question_collection_id" => $this->questionCollection->id]), true);
    }

    public function delete()
    {
        $this->question->content = $this->questionContent;
        $this->question->question_status_id = 2;
        $this->question->save();
        
        for ($i = 0; $i < 4; $i++) {
            $this->question->answersTrue[$i]->content = $this->answers[$i];
            $this->question->answersTrue[$i]->save();
        }

        return $this->redirect(route("moderator.question-collections.show", ["classroom_id" => $this->classroom->id, "question_collection_id" => $this->questionCollection->id]), true);
    }

    public function restore()
    {
        $this->question->content = $this->questionContent;
        $this->question->question_status_id = 1;
        $this->question->save();
        
        for ($i = 0; $i < 4; $i++) {
            $this->question->answersTrue[$i]->content = $this->answers[$i];
            $this->question->answersTrue[$i]->save();
        }

        return $this->redirect(route("moderator.question-collections.show", ["classroom_id" => $this->classroom->id, "question_collection_id" => $this->questionCollection->id]), true);
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.questions.moderator-question-edit-livewire');
    }
}