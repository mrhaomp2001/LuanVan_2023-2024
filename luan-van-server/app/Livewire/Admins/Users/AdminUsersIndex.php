<?php

namespace App\Livewire\Admins\Users;

use App\Models\User;
use Livewire\Component;
use Livewire\WithPagination;
class AdminUsersIndex extends Component
{
    use WithPagination;
    public $query;

    public function updated($property)
    {
        // $property: The name of the current property that was updated
 
        if ($property === 'query') {
            $this->resetPage();
        }
    }

    public function render()
    {
        return view('livewire.admins.users.admin-users-index', 
        [
            'users' => User::where('name', 'like', '%'.$this->query.'%')->orWhere('username', 'like', '%'.$this->query.'%')->paginate(5),
        ]);
    }
}
