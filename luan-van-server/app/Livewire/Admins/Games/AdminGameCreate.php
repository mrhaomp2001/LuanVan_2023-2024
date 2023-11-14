<?php

namespace App\Livewire\Admins\Games;

use App\Models\Game;
use Livewire\Component;

class AdminGameCreate extends Component
{
    public $name;
    public $description;
    
    public $game;

    public function save() {
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
