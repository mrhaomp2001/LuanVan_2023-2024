<?php

namespace App\Livewire\Admins\Games;

use App\Models\Game;
use Livewire\Component;

class AdminGameCreate extends Component
{
    public $name;
    public $description;

    public $game;

    public function save()
    {
        $this->validate(
            [
                'name' => ["required", "min:3", 'max:64'],
                'description' => ["required", "min:3", 'max:512'],
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'description.required' => "Cần nhập miêu tả",
                'description.min' => "Cần nhập miêu với tối thiểu :min ký tự",
                'description.max' => "Cần nhập miêu với tối đa :max ký tự",

            ]
        );
        $this->game = new Game();

        $this->game->name = $this->name;
        $this->game->description = $this->description;
        $this->game->save();

        $this->redirect(route("admin.game.index"));
    }

    public function render()
    {
        return view('livewire.admins.games.admin-game-create');
    }
}
