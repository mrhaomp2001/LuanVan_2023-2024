<?php

namespace App\Livewire\Admins\Games;

use App\Models\Game;
use Livewire\Component;

class AdminGameEdit extends Component
{
    public $name;
    public $description;
    
    public $game;

    public function mount($game_id)
    {
        $this->game = Game::find($game_id);

        $this->name = $this->game->name;
        $this->description = $this->game->description;
    }

    public function save() {
        $this->game->name = $this->name;
        $this->game->description = $this->description;
        $this->game->save();

        $this->redirect(route("admin.game.index"));
    }

    public function render()
    {
        return view('livewire.admins.games.admin-game-edit');
    }
}
