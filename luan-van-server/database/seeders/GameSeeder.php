<?php

namespace Database\Seeders;

use App\Models\Game;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class GameSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //

        Game::create([
            'name' => "Chiến đấu quái vật",
            'description' => "Chiến đấu quái vật",
        ]);

        Game::create([
            'name' => "Vượt chứa ngại vật",
            'description' => "Vượt chứa ngại vật",
        ]);
    }
}
