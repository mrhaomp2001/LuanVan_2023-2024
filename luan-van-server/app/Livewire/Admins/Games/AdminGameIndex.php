<?php

namespace App\Livewire\Admins\Games;

use App\Models\Game;
use Livewire\Component;

class AdminGameIndex extends Component
{
    public $games;

    public function mount()
    {
        $this->games = Game::all();
    }

    public function render()
    {
        return view('livewire.admins.games.admin-game-index');
    }
}
