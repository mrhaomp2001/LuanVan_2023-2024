<?php

namespace App\Livewire\Admins\Classrooms;

use App\Models\Classroom;
use Livewire\Component;

class AdminClassroomsIndex extends Component
{
    public function mount()
    {
    }

    public function render()
    {
        return view('livewire.admins.classrooms.admin-classrooms-index', 
        [
            'classrooms' => Classroom::paginate(10),
        ]);
    }
}
