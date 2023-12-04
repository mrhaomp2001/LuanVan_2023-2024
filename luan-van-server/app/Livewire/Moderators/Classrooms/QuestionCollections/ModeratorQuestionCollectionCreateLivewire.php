<?php

namespace App\Livewire\Moderators\Classrooms\QuestionCollections;

use App\Models\Classroom;
use App\Models\Game;
use App\Models\QuestionCollection;
use Livewire\Component;

class ModeratorQuestionCollectionCreateLivewire extends Component
{
    public Classroom $classroom;
    public $games;

    public $name;
    public $difficulty;
    public $questions_per_time;
    public $game_id;

    public function mount($classroom_id)
    {
        $this->classroom = Classroom::findOrFail($classroom_id);

        if ($this->classroom->user_id != auth()->user()->id) {
            return redirect(route("404"));
        }

        $this->games = Game::all();
        $this->game_id = 0;
    }

    public function save()
    {
        $this->validate(
            [
                'name' => 'required|min:3|max:64',
                'difficulty' => 'required|min:3|max:128',
                'questions_per_time' => 'required|numeric|min:4|max:16',
                'game_id' => "required|exists:games,id",
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'difficulty.required' => "Cần nhập độ khó",
                'difficulty.min' => "Cần nhập độ khó với tối thiểu :min ký tự",
                'difficulty.max' => "Cần nhập độ khó với tối đa :max ký tự",

                'questions_per_time.required' => "Nhập số câu hỏi cần làm",
                'questions_per_time.numeric' => "Nhập số câu hỏi cần làm",
                'questions_per_time.min' => "Cần nhập số câu hỏi với tối thiểu :min câu",
                'questions_per_time.max' => "Cần nhập số câu hỏi với tối đa :max câu",

                'game_id.required' => "Cần chọn trò chơi",
                'game_id.exists' => "Trò chơi không tồn tại",
            ]
        );

        QuestionCollection::create([
            'classroom_id' => $this->classroom->id,
            'name' => $this->name,
            'difficulty' => $this->difficulty,
            'questions_per_time' => $this->questions_per_time,
            'game_id' => $this->game_id,
        ]);

        return redirect(route("moderator.classrooms.show", ['id' => $this->classroom->id]));
    }

    public function render()
    {
        return view('livewire.moderators.classrooms.question-collections.moderator-question-collection-create-livewire');
    }
}