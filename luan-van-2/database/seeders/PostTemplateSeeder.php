<?php

namespace Database\Seeders;

use App\Models\PostTemplate;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class PostTemplateSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        for ($i=1; $i < 4; $i++) { 
            PostTemplate::create(
                [
                    'name' => "Mẫu số " . $i,
                    'content' => "Nội dung quy định mẫu số " . $i,
                    'theme_color' => "#141e28"
                ]
            );
        }

    }
}
