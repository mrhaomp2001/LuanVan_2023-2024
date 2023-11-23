<?php

namespace App\Livewire\Admins\Moderators;

use App\Models\User;
use Livewire\Component;
use Livewire\WithPagination;
use Livewire\Attributes\Renderless;

class AdminModeratorShow extends Component
{
    use WithPagination;
    public $searchQuery;


    public function grantPermissions($id)
    {
        $user = User::find($id);
        $user->role_id = 2;
        $user->save();
        
        $this->reset();
    }

    public function deletePermissions($id)
    {
        $user = User::find($id);
        $user->role_id = 1;
        $user->save();

        $this->reset();
    }

    public function updated($property)
    {
        if ($property === 'searchQuery') {
            $this->resetPage('users-page');
        }
    }

    public function render()
    {
        return view('livewire.admins.moderators.admin-moderator-show', 
        [
            'users' => User::where('role_id', 1)
            ->where(function ($query) {
                $query->where('username', 'like', '%' . $this->searchQuery . '%')
                    ->orWhere('name', 'like', '%' . $this->searchQuery . '%');
            })
            ->paginate(7, pageName: 'users-page'),
            'moderators' => User::where("role_id", 2)->paginate(7, pageName: 'moderators-page'),
        ]);
    }
}
